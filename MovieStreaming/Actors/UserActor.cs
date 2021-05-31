using Akka.Actor;
using MovieStreaming.Messages;
using System;

namespace MovieStreaming.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("Creating a user actor");

            ColorConsole.WriteLineCyan("Setting initial behaviour to stopped.");
            Stopped();
        }


        private void Stopped()
        {
            Receive<PlayMovieMessage>(msg=> StartPlayingMovie(msg.MovieTitle));
            Receive<StopMovieMessage>(msg=>ColorConsole.WriteLineRed("Error: Cannot stop if nothing is playing"));

            ColorConsole.WriteLineCyan("User actor has now become stopped");
        }


        private void Playing()
        {
            Receive<PlayMovieMessage>(msg=> ColorConsole.WriteLineRed(
                    "Error: Cannot start playing another moviebefore stopping existing one"
                    ));
            Receive<StopMovieMessage>(msg => StopPlayingCurrentMovie());

            ColorConsole.WriteLineCyan("User actor has now become Playing");
        }

        private void StartPlayingMovie(string title)
        {
            _currentlyWatching = title;
            ColorConsole.WriteLineYellow(string.Format("User is currently watching '{0}'",_currentlyWatching));
            Become(Playing);

        }

        private void StopPlayingCurrentMovie()
        {
            ColorConsole.WriteLineYellow(string.Format("User has stopped watching '{0}'",_currentlyWatching));
            _currentlyWatching = null;
            Become(Stopped);
        }

        //private void HandlePlayMovieMessage(PlayMovieMessage message)
        //{
        //    if (_currentlyWatching != null)
        //    {
        //        ColorConsole.WriteLineRed(
        //            "Error: Cannot start playing another moviebefore stopping existing one"
        //            );
        //    }
        //    else
        //    {
        //        StartPlayingMovie(message.MovieTitle);
        //    }
        //}

        //private void HandleStopMovieMessage()
        //{
        //    if(_currentlyWatching!=null)
        //    {
        //        ColorConsole.WriteLineRed("Error: Cannot stope if anything is not playing");
        //    }
        //    else
        //    {
        //        StopPlayingCurrentMovie();
        //    }
        //}
        protected override void PreStart()
        {
            ColorConsole.WriteLineGreen("UserActor PresStart");
        }
        protected override void PostStop()
        {
            ColorConsole.WriteLineGreen("UserActor PostStop");
        }

        protected override void PreRestart(Exception reason, object message)
        {
            ColorConsole.WriteLineGreen("UserActor Prerestart because: " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineGreen("UserActor Postrestart because: " + reason);
            base.PostRestart(reason);
        }
    }
}
