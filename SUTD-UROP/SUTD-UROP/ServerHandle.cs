using System;
using System.Collections.Generic;
using System.Text;

namespace SUTD_UROP
{
    class ServerHandle
    {
        public static void WelcomeReceived(int _fromClient, Packet _packet)
        {
            // read message based on structure , can use this for raeding data like time or score
            int _clientIdCheck = _packet.ReadInt();
            string _clientName = _packet.ReadString();
            Console.WriteLine($"{Server.clients[_fromClient].tcp.socket.Client.RemoteEndPoint} ({_clientName}) connected successfully and is now player {_fromClient}.");
            if (_fromClient != _clientIdCheck)
            {
                Console.WriteLine($"Player (ID: {_fromClient}) has assumed the wrong client ID ({_clientIdCheck})!");
            }
            else
            {
                Server.roomlist[0].Participant.Add(_fromClient, false);
                Server.roomlist[0].result.Add(_fromClient, null);
                Server.clients[_fromClient].username = _clientName;
            }
            // TODO: send player into game
        }
        public static void Readresult(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            float _time_result = _packet.ReadFloat();
            if (_fromClient == _clientIdCheck)
            {
                
                Server.roomlist[0].result[_fromClient] = _time_result;
                Server.roomlist[0].sorting_result.Add(_time_result);
                Console.WriteLine($"Player { _clientIdCheck} time result have been updated. ( used time:{_time_result})");
            }
            else
            {
                Console.WriteLine($"Player ID incorrect.");
            }
        }
        public static void readyRecieved(int _fromClient, Packet _packet)
        {
            int _clientIdCheck = _packet.ReadInt();
            if (_fromClient == _clientIdCheck)
            {
                Console.WriteLine($"Player {_fromClient} has ready.");
                if (Server.roomlist[0].checkStatus(_fromClient))
                {
                    ServerSend.SendQuestion();
                    Console.WriteLine($"question sent. ");
                }
            }
            else
            {
                Console.WriteLine($"Player ID incorrect.");
            }
        }
    }
}