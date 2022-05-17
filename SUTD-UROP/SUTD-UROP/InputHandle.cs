using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
namespace SUTD_UROP
{
    class InputHandle
    {

        public static async void ServerInput()
        {
            Task input = Task.Run(() => { handleInput(); });
            await input;
        }
        private static void handleInput()
        {
            try
            {
               string input=Console.ReadLine();
                switch (input)
                {
                    case "start match":
                        {
                            if (Server.roomlist[0].Participant.Count <= 0)
                            {
                                Console.WriteLine($"There is not enough client inside the room ! cannot start match...");
                                Console.WriteLine($"Return to main..");
                                break;
                            }
                            ServerSend.startmatch();
                            Console.WriteLine($"match has started");
                        }
                        break;
                    default:
                        { 
                        }
                        break;
                }
            }
            catch
            {
                Console.WriteLine($"Something Wrong happen!! Return to entry..");
            }
            ServerInput();
        }
    }
}
