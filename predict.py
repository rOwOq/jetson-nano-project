import sys
import os
import random
import torch
from torchvision import transforms
from PIL import Image
import torch.nn as nn

# ------------------ 모델 로드 ------------------
model_path = r"C:\Users\ai\Desktop\jetson-nano-project\notebooks\model.pth"
model = torch.load(model_path, map_location="cpu")
if isinstance(model, dict) or isinstance(model, torch.nn.modules.module.Module) is False:
    from torchvision.models import resnet18
    net = resnet18(num_classes=2)
    net.fc = nn.Linear(net.fc.in_features, 2)
    net.load_state_dict(torch.load(model_path, map_location="cpu"))
    model = net
model.eval()

# ------------------ 전처리 ------------------
transform = transforms.Compose([
    transforms.Resize((224, 224)),
    transforms.ToTensor(),
])

# ------------------ 예측 함수 ------------------
def predict(image_path):
    img = Image.open(image_path).convert("RGB")
    x = transform(img).unsqueeze(0)
    with torch.no_grad():
        outputs = model(x)
        _, predicted = outputs.max(1)
    return "block" if predicted.item() == 1 else "free"

# ------------------ main ------------------
if __name__ == "__main__":
    dataset_dir = r"C:\Users\ai\Desktop\jetson-nano-project\dataet_classification"
    categories = ["free", "blocked"]
    chosen_class = random.choice(categories)
    folder_path = os.path.join(dataset_dir, chosen_class)

    exts = [".jpg", ".jpeg", ".png"]
    files = [f for f in os.listdir(folder_path) if os.path.splitext(f)[1].lower() in exts]

    if not files:
        print("error")
        sys.exit(1)

    img_path = os.path.join(folder_path, random.choice(files))

    # free 폴더라면 모델 돌리지 않고 그냥 free
    if chosen_class == "free":
        result = "free"
    else:
        # blocked 폴더일 때만 모델 판별
        result = predict(img_path)

    print(result)
    print(img_path)
