using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Threading.Tasks; // 상단 using에 추가

namespace RoadSimulation
{
    public partial class Form1 : Form
    {
        private Timer carTimer;
        private Timer blinkTimer;
        private Timer predictTimer;

        private int carSpeed = 5;   // 🚗 기본 속도
        private int carDirection = 1;

        public Form1()
        {
            InitializeComponent();
            InitSimulation();
            LoadTrainLog();        // CSV 학습 로그 그래프 로드
            StartAutoPrediction(); // 자동 예측 시작
        }
        private volatile bool isPredicting = false; // 재진입 방지
        // ---------------- CSV 학습 로그 ----------------
        private void LoadTrainLog()
        {
            string csvPath = @"C:\Users\ai\Desktop\jetson-nano-project\notebooks\train_log.csv";
            if (!File.Exists(csvPath)) return;

            chart1.Series.Clear();
            chart1.ChartAreas.Clear();
            chart1.ChartAreas.Add(new ChartArea("Default"));

            var accSeries = chart1.Series.Add("Accuracy");
            var lossSeries = chart1.Series.Add("Loss");
            accSeries.ChartType = SeriesChartType.Line;
            lossSeries.ChartType = SeriesChartType.Line;

            var lines = File.ReadAllLines(csvPath);
            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split(',');
                if (parts.Length < 2) continue;

                double loss = double.Parse(parts[0]);
                double acc = double.Parse(parts[1]);

                int epoch = i + 1;
                lossSeries.Points.AddXY(epoch, loss);
                accSeries.Points.AddXY(epoch, acc);
            }
        }

        // ---------------- 파이썬 실행 ----------------
        private string[] RunPythonPredict(string folderPath)
        {
            string pythonExe = @"C:\Users\ai\AppData\Local\Programs\Python\Python313\python.exe";
            string scriptPath = @"C:\Users\ai\Desktop\jetson-nano-project\predict.py";

            var psi = new ProcessStartInfo
            {
                FileName = pythonExe,
                Arguments = $"\"{scriptPath}\" \"{folderPath}\"",
                RedirectStandardOutput = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

            using (var process = Process.Start(psi))
            {
                string output = process.StandardOutput.ReadToEnd();
                process.WaitForExit();
                return output.Split(new[] { '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
            }
        }

        // ---------------- 자동 예측 ----------------
        private void StartAutoPrediction()
        {
            predictTimer = new Timer { Interval = 3000 }; // 3초마다 실행
            predictTimer.Tick += async (s, e) =>
            {
                if (isPredicting) return;   // 재진입 방지
                isPredicting = true;

                string datasetRoot = @"C:\Users\ai\Desktop\jetson-nano-project\dataet_classification";

                // 백그라운드에서 파이썬 실행
                string[] lines = await Task.Run(() => RunPythonPredict(datasetRoot));

                if (lines.Length >= 2)
                {
                    string result = lines[0].Trim();   // "free" / "block"
                    string imgPath = lines[1].Trim();

                    // UI 업데이트는 메인 스레드에서
                    if (this.IsHandleCreated)
                    {
                        this.BeginInvoke(new Action(() =>
                        {
                            lblResult.Text = "예측 결과: " + result;

                            if (pictureBoxInput.Image != null)
                                pictureBoxInput.Image.Dispose();

                            try
                            {
                                using (var fs = new FileStream(imgPath, FileMode.Open, FileAccess.Read))
                                {
                                    pictureBoxInput.Image = Image.FromStream(fs);
                                }
                                pictureBoxInput.SizeMode = PictureBoxSizeMode.Zoom;
                            }
                            catch { /* 이미지 파일 잠김 등 예외 무시 */ }

                            HandlePredictionResult(result); // carSpeed 제어 (free=5, block=0)
                        }));
                    }
                }

                isPredicting = false;
            };
            predictTimer.Start();
        }
        // ---------------- 시뮬레이션 ----------------
        private void InitSimulation()
        {
            // 자동차
            pictureBoxCar.Image = Image.FromFile(@"C:\Users\ai\Desktop\jetson-nano-project\images\car.jpg");
            pictureBoxCar.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBoxCar.Width = 100;
            pictureBoxCar.Height = 60;
            pictureBoxCar.Left = 0;
            pictureBoxCar.Top = (panelRoad.Height / 2) - (pictureBoxCar.Height / 2);

            // 🚧 경고판 (노란 박스 제거)
            labelBlocked.Text = "🚧 BLOCKED";
            labelBlocked.Font = new Font("Arial", 24, FontStyle.Bold);
            labelBlocked.ForeColor = Color.Red;
            labelBlocked.BackColor = Color.Transparent;
            labelBlocked.AutoSize = true;
            labelBlocked.Visible = false;
            panelRoad.Controls.Add(labelBlocked);
            labelBlocked.BringToFront();

            // 자동차 타이머: 항상 실행 (속도로 제어)
            carTimer = new Timer { Interval = 50 };
            carTimer.Tick += CarTimer_Tick;
            carTimer.Start();
        }

        // 자동차 이동: 항상 돌고, 멈춤/주행은 carSpeed로만 제어
        private void CarTimer_Tick(object sender, EventArgs e)
        {
            pictureBoxCar.Left += carSpeed * carDirection;

            // 벽 반사
            if (pictureBoxCar.Right >= panelRoad.Width || pictureBoxCar.Left <= 0)
                carDirection *= -1;
        }

        // 예측 결과 처리: block이면 멈춤, free면 달리기
        private void HandlePredictionResult(string result)
        {
            if (result.Contains("block"))
            {
                // 멈춤
                carSpeed = 0;

                // 경고판 (차 위쪽)
                labelBlocked.Left = pictureBoxCar.Left;
                labelBlocked.Top = pictureBoxCar.Top - 40;
                labelBlocked.Visible = true;
                labelBlocked.BringToFront();

                // 깜빡임
                if (blinkTimer == null)
                {
                    blinkTimer = new Timer { Interval = 1000 };
                    blinkTimer.Tick += (s, e) => { labelBlocked.Visible = !labelBlocked.Visible; };
                }
                blinkTimer.Start();
            }
            else
            {
                // free는 반응 안 한다 = 계속 달림
                carSpeed = 5;                 // 달리기 복귀
                if (blinkTimer != null)       // 경고 끄기
                    blinkTimer.Stop();
                labelBlocked.Visible = false;
            }
        }
    }
}
