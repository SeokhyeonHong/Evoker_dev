import cv2
from fer import FER
import socket
import os

cap = cv2.VideoCapture(0)

fourcc = cv2.VideoWriter_fourcc(*'DIVX')
out = cv2.VideoWriter(os.path.dirname(os.path.realpath(__file__)) + '\\output.avi', fourcc, 25.0, (1920, 1080))

emotion_detector = FER()

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect(("127.0.0.1", 50003))

while True: 
    retval, frame = cap.read()
    if not(retval):
        break

    result = emotion_detector.detect_emotions(frame)
    
    if len(result) > 0:
        emotions = result[0]['emotions']

        val = ""
        text = ""
        for index, (emotion_name, score) in enumerate(emotions.items()):
            val += "{:.2f}".format(score)
            text = emotion_name + ": " + str(score)
            cv2.putText(frame, text, (0, 30 + 15 * index), cv2.FONT_HERSHEY_SIMPLEX, 0.5, (255, 0, 0), 1, cv2.LINE_AA,)
        # val = emotion_name + ' ' + str(emotion_score)
        
        sock.send(val.encode())

    out.write(frame)
    cv2.waitKey(33)