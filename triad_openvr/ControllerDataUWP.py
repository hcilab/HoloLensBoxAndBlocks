import triad_openvr
import time
import sys
import socket

#connect and info on VR stuff - controllers
v = triad_openvr.triad_openvr()
v.print_discovered_objects()

#setting the sampling rate
if len(sys.argv) == 1:
    interval = 1/45
elif len(sys.argv) == 2:
    interval = 1/float(sys.argv[1])
else:
    print("Invalid number of arguments")
    interval = False
    

#create socket server
host = '0.0.0.0'
port = 5555

print('the host name is: ' + socket.gethostname())

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
s.setsockopt(socket.IPPROTO_TCP, socket.TCP_NODELAY,1)
try:
    s.bind((host,port))
except socket.error as e:
    print(str(e))

s.listen(1)
print('waiting for a connection.')
conn, addr = s.accept()

print('connected to: '+addr[0]+':'+str(addr[1]))

msg = ''

if interval:
    while(True):
        start = time.time()
        txt = ""
        msg = ''
        for each in v.devices["controller_1"].get_pose_quaternion():
            txt = "%.4f" % each
            msg = msg + txt + ' '
        conn.sendall(str.encode(msg + '\n'))
        sleep_time = interval-(time.time()-start)
        if sleep_time>0:
            time.sleep(sleep_time)
    conn.close()
