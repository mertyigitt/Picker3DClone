using Runtime.Data.ValueObjects;
using Runtime.Managers;

namespace Runtime.Commands.Player
{
    public class ForceBallsToPoolCommand
    {
        private PlayerManager _manager;
        private playerForceData _forceData;
        
        public ForceBallsToPoolCommand(PlayerManager manager, playerForceData forceData)
        {
            _manager = manager;
            _forceData = forceData;
        }

        internal void Execute()
        {
            
        }
    }
}