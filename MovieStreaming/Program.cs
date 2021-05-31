using System;
using Akka.Actor;
using MovieStreaming.Actors;
using MovieStreaming.Messages;

namespace MovieStreaming
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;
        static void Main(string[] args)
        {
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor system created");


            //Props playbackActorProps = Props.Create<PlaybackActor>();
            //IActorRef playbackActorRef = MovieStreamingActorSystem.ActorOf(playbackActorProps, "PlaybackActor");

            Props userActorProps = Props.Create<UserActor>();
            IActorRef userActorRef = MovieStreamingActorSystem.ActorOf(userActorProps, "UserActor");

            Console.ReadKey();
            Console.WriteLine("Sending aplay movie message (Codenan the Destroyer)");
            userActorRef.Tell(new PlayMovieMessage("Akka.Net: Codenan the Destroyer", 45));

            Console.ReadKey();
            Console.WriteLine("Sending a play movie message (Boolean Lies)");
            userActorRef.Tell(new PlayMovieMessage("Akka.Net: Boolean Lies", 45));

            Console.ReadKey();
            Console.WriteLine("Sending a Stop movie message");
            userActorRef.Tell(new StopMovieMessage());

            Console.ReadKey();
            Console.WriteLine("Sending a another Stop movie message");
            userActorRef.Tell(new StopMovieMessage());

            //playbackActorRef.Tell(new PlayMovieMessage("Akka.Net: The Movie", 42));
            //playbackActorRef.Tell(new PlayMovieMessage("Akka.Net: Partial Recall", 43));
            //playbackActorRef.Tell(new PlayMovieMessage("Akka.Net: Boolean Lies", 44));
            //playbackActorRef.Tell(new PlayMovieMessage("Akka.Net: Codenan the Destroyer", 45));

            //playbackActorRef.Tell(PoisonPill.Instance);

            Console.ReadLine();
            MovieStreamingActorSystem.Shutdown();
            MovieStreamingActorSystem.AwaitTermination();
            Console.WriteLine("Actor SystemShutdown");

            Console.ReadKey();
        }
    }
}
