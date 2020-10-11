using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPA_Mech.Utils
{
    public abstract class DialogVmBase : PropertyChangedNotifier
    {
        public bool IsCreate { get; protected set; }
        public bool Result { get; protected set; }

        public abstract string WindowTitle { get; }

        public event EventHandler Closed;

        protected virtual void RaiseClosed()
        {
            var handler = Closed;
            if (handler != null) handler(this, EventArgs.Empty);
        }

        private ActionCommand _applyCommand;
        public ActionCommand ApplyCommand
        {
            get
            {
                return _applyCommand ?? (_applyCommand = new ActionCommand(Apply, CanApply));
            }
        }

        protected virtual void Apply()
        {
            Result = true;
            RaiseClosed();
        }

        protected virtual bool CanApply()
        {
            return true;
        }
    }
}
