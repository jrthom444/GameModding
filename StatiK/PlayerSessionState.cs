using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StatiK.Utils;

namespace StatiK
{
    public enum PlayerAction {ROLLOUT_FLIGHT, EDITOR_REVERT, FLIGHT_REVERT, RECOVER_FLIGHT, FLYING, SWITCH_FLYING, EDITING, IDLE, START};

    public class PlayerSessionState
    {
        private ILogger log = StatikLogManager.Instance.GetLogger(typeof(PlayerSessionState).Name);
        public PlayerAction LastPlayerAction{ get; set; }
        public PlayerAction CurrentPlayerAction { get; set; }

        public PlayerSessionState()
        {
            LastPlayerAction = PlayerAction.IDLE;
            CurrentPlayerAction = PlayerAction.IDLE;
            log.Debug(this.ToString());
        }

        public void FlightRolledOut()
        {
            CurrentPlayerAction = PlayerAction.ROLLOUT_FLIGHT;
            log.Debug(this.ToString());
        }

        public void FlightRecovered()
        {
            CurrentPlayerAction = PlayerAction.RECOVER_FLIGHT;
            log.Debug(this.ToString());
        }

        public void SwitchedVessels()
        {
            if (LastPlayerAction != PlayerAction.IDLE)
            {
                CurrentPlayerAction = PlayerAction.SWITCH_FLYING;
                log.Debug(this.ToString());
            }
        }

        public void SceneChanged(GameScenes from, GameScenes to)
        {
            LastPlayerAction = (PlayerAction)((int)CurrentPlayerAction);

            if(from == GameScenes.MAINMENU && to == GameScenes.SPACECENTER)
            {
                LastPlayerAction = PlayerAction.START;
                CurrentPlayerAction = PlayerAction.START;
            }
            else if(from == GameScenes.FLIGHT && to == GameScenes.EDITOR)
            {
                LastPlayerAction = PlayerAction.FLYING;
                CurrentPlayerAction = PlayerAction.EDITOR_REVERT;
            }
            else if(from == GameScenes.FLIGHT && to == GameScenes.FLIGHT && CurrentPlayerAction != PlayerAction.SWITCH_FLYING)
            {
                LastPlayerAction = PlayerAction.FLYING;
                CurrentPlayerAction = PlayerAction.FLIGHT_REVERT;
            }
            else if(to == GameScenes.SPACECENTER && CurrentPlayerAction != PlayerAction.RECOVER_FLIGHT)
            {
                CurrentPlayerAction = PlayerAction.IDLE;
            }
            else if(to == GameScenes.EDITOR)
            {
                CurrentPlayerAction = PlayerAction.EDITING;
            }
            else if(to == GameScenes.FLIGHT)
            {
                CurrentPlayerAction = PlayerAction.FLYING;
            }
            else
            {
                CurrentPlayerAction = PlayerAction.IDLE;
            }
            log.Debug(this.ToString());
        }

        public override string ToString()
        {
            return string.Format("PlayerSessionState[LastAction: {0}, CurrentAction: {1}]", LastPlayerAction, CurrentPlayerAction);
        }
    }
}
