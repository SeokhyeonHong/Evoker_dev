import cv2
from fer import FER
import socket
import UnityEngine as ue

objects = ue.Object.FindObjectsOfType(ue.GameObject)
for go in objects:
    ue.Debug.Log(go.name)

# cap = cv2.VideoCapture(0)
# emotion_detector = FER()

sock = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
sock.connect(("127.0.0.1", 50003))
val = 0.01
sock.send(val.encode())

'''
while True:
    retval, frame = cap.read()

    result = emotion_detector.detect_emotions(frame)
    if len(result) > 0:
        emotions = result[0]['emotions']

        val = ""
        for score in emotions.values():
            val += "{:.2f}".format(score)
        # val = emotion_name + ' ' + str(emotion_score)
        
        # sock.send(val.encode())
        ue.Debug.Log("Successful")
    ue.Debug.Log("HI")
'''