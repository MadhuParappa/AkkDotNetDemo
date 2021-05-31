using System;
using Akka.Actor;
using MovieStreaming.Messages;

namespace MovieStreaming.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Console.WriteLine("Playback actor created");
            Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
        }

        private void HandlePlayMovieMessage(PlayMovieMessage message)
        {
            ColorConsole.WriteLineYellow(
                string.Format("Play Movie Mesage '{0}' for user '{1}'", message.MovieTitle, message.UserId)
                );

        }

        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("PlaybackActor PresStart");
        }
        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("PlaybackActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen("PlaybackActor Prerestart because: "+ reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineGreen("PlaybackActor Postrestart because: " + reason);
            base.PostRestart(reason);
        }
    }
}
