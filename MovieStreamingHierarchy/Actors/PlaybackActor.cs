using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Akka.Actor;
using MovieStreamingHierarchy.Messages;

namespace MovieStreamingHierarchy.Actors
{
    public class PlaybackActor:ReceiveActor
    {
        public PlaybackActor()
        {
           Context.ActorOf(Props.Create<UserCoordinatorActor>(),"UserCoordinator");
           Context.ActorOf(Props.Create<PlaybackStatisticsActor>(),"PlaybackStatistics");
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
            ColorConsole.WriteLineGreen("PlaybackActor Prerestart because: " + reason);
            base.PreRestart(reason, message);
        }

        protected override void PostRestart(Exception reason)
        {
            ColorConsole.WriteLineGreen("PlaybackActor Postrestart because: " + reason);
            base.PostRestart(reason);
        }
    }
}
