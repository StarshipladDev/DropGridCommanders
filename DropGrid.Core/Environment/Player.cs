using System;
namespace DropGrid.Core.Environment
{
    public class Player
    {
        public string _name;
        public string Name { get { return _name;  } }

        public Player(string name)
        {
            this._name = name;
        }
    }
}
