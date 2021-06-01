using Akka.Actor;
using MovieStreamingHierarchy.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieStreamingHierarchy.Actors
{
    public class UserCoordinatorActor:ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;
        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(msg=>
            {
                CreateChildIfUserNotExists(msg.UserId);
                IActorRef childActorRef = _users[msg.UserId];
                childActorRef.Tell(msg);
            });
        }

        private void CreateChildIfUserNotExists(int userId)
        {
            if(!_users.ContainsKey(userId))
            {
                IActorRef newChildActorRef = Context.ActorOf(Props.Create(() => new UserActor(userId)), "User" + userId);
                _users.Add(userId,newChildActorRef);
                ColorConsole.WriteLineCyan("UserCoordinatorActor created new child UserACtor for {0} (Total users: {1})",userId,_users.Count());
            }
        }
    }
}
