using System;
using System.Collections.Generic;
using System.Text;

namespace SUTD_UROP
{
    class ServerSend
    {
        private static void SendTCPData(int _toClient, Packet _packet)
        {
            _packet.WriteLength();
            Server.clients[_toClient].tcp.SendData(_packet);
        }

        private static void SendTCPDataToAll(Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                Server.clients[i].tcp.SendData(_packet);
            }
        }
        private static void SendTCPDataToAll(int _exceptClient, Packet _packet)
        {
            _packet.WriteLength();
            for (int i = 1; i <= Server.MaxPlayers; i++)
            {
                if (i != _exceptClient)
                {
                    Server.clients[i].tcp.SendData(_packet);
                }
            }
        }

        public static void Welcome(int _toClient, string _msg)
        {
            using (Packet _packet = new Packet((int)ServerPackets.welcome))
            {
                _packet.Write(_msg);
                _packet.Write(_toClient);

                SendTCPData(_toClient, _packet);
            }
        }
        public static void startmatch()
        {
            using (Packet _packet = new Packet((int)ServerPackets.startmatch))
            {
                SendTCPDataToAll(_packet);
            }
        }
        public static void SendQuestion()
        {
            using (Packet _packet = new Packet((int)ServerPackets.sendQuestion))
            {
                 _packet.Write(Server.roomlist[0].sequenceString);
                SendTCPDataToAll(_packet);
            }
        }

        public static void SendResult(int _toClient , float _result , int rank)
        {
            using (Packet _packet = new Packet((int)ServerPackets.SendResultToAll))
            {
                _packet.Write(_result);
                _packet.Write(rank);
                SendTCPData(_toClient,_packet);
            }
        }
    }
}