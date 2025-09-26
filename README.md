# Jetson Nano Project

Jetson Nano 환경과 Python 딥러닝 모델을 활용하여
이미지를 분석하고 C# WinForms 애플리케이션과 연동하는 프로젝트입니다.

## ⚙️ 주요 기능

## 📷 이미지 캡처/선택 → WinForms에서 카메라 이미지 또는 파일 선택

## 🤖 AI 모델 추론 → Python(predict.py) 실행 후 결과 반환

## 🔗 C# ↔ Python 연동 → Process.Start()로 Python 스크립트 실행, 결과를 WinForms에서 읽어 출력

## 📊 예측 결과 시각화 → WinForms UI에서 즉시 표시

## 📂 프로젝트 구조
jetson-nano-project/
├─ predict.py            # Python 추론 스크립트 (PIL, Torch/Tensorflow 등 활용)
├─ requirements.txt      # Python 패키지 목록
├─ Form1.cs              # WinForms 메인 UI
├─ Program.cs            # C# 엔트리 포인트
└─ README.md

## 🛠️ 사용 기술

Python : Pillow, Torch/TensorFlow, OpenCV 등 (이미지 처리 & 추론)

# C# (WinForms) : UI 및 Python 연동

Jetson Nano : 모델 실행 최적화 (GPU 가속)

## 🚀 실행 방법
1. Python 환경 준비
## 가상환경 추천
python -m venv venv
source venv/bin/activate   # (윈도우는 venv\Scripts\activate)

pip install -r requirements.txt

2. C# 프로젝트 실행

Visual Studio에서 Form1.cs 열고 실행

내부에서 predict.py를 호출하여 결과 출력

3. 경로 설정 확인

## C# 코드에서 Python 실행 경로와 스크립트 경로를 수정해야 합니다:

string pythonExe = @"C:\Users\ai\anaconda3\python.exe";  
string scriptPath = @"C:\Users\ai\Desktop\jetson-nano-project\predict.py";

## 📌 주의 사항

Jetson Nano에서 실행할 경우 Python 경로(/usr/bin/python3)와 의존 패키지 설치 확인 필요

Windows 환경에서는 Anaconda Python 경로를 맞게 지정해야 함

입력 이미지는 .jpg, .png 지원 (predict.py 내부에서 수정 가능)

## 👨‍💻 개발자

김병철
시뮬레이션 시연 영상 : https://blog.naver.com/ehowl169/224022916922
<br>2025년 Jetson Nano AI 프로젝트
