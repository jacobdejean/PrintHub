using System;

namespace Framework.Validation
{
    class StateValidator
    {
        private PrintHubManager State;

        public StateValidator(PrintHubManager State)
        {
            this.State = State;
        }

        public bool ValidateTrack(string name)
        {
            return State.TrackedFiles.Exists((p) => p.Name.Equals(name));
        }
    }
}