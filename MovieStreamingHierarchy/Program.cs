using Akka.Actor;
using MovieStreamingHierarchy.Actors;
using MovieStreamingHierarchy.Messages;
using System;

namespace MovieStreamingHierarchy
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;
        static void Main(string[] args)
        {
            ColorConsole.WriteLineGray("Creating a MovieStreamingActorSytem");
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSytem");

            ColorConsole.WriteLineGray("Creating actor SupervisoryHierarchy");
            MovieStreamingActorSystem.ActorOf(Props.Create<PlaybackActor>(),"Playback");

            do
            {
                //ShutDown();

                Console.WriteLine();
                Console.ForegroundColor = ConsoleColor.DarkGray;
                ColorConsole.WriteLineGray("Enter acommand and hit a enter");

                var command = Console.ReadLine();

                if(command.StartsWith("Play"))
                {
                    int userId = int.Parse(command.Split(',')[1]);
                    string movieTitle = command.Split(',')[2];

                    var message = new PlayMovieMessage(movieTitle,userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);

                }

                if (command.StartsWith("Stop"))
                {
                    int userId = int.Parse(command.Split(',')[1]);

                    var message = new StopMovieMessage(userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if(command=="exit")
                {
                    MovieStreamingActorSystem.Shutdown();
                    MovieStreamingActorSystem.AwaitTermination();
                    ColorConsole.WriteLineGray("Actor system shutdown");
                    Console.ReadKey();
                    Environment.Exit(1);
                }

            } while (true);
            
        }
    }
}
