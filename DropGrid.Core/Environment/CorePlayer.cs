using System;

namespace DropGrid.Core.Environment
{
    public abstract class CorePlayer
    {
        public string _name;
        public string Name { get { return _name;  } }

        public CorePlayer(string name)
        {
            this._name = name;
        }
    }
}