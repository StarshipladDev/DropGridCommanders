using System;
namespace DropGrid.Core.Environment
{
    public class GamePlayer
    {
        public string _name;
        public string Name { get { return _name;  } }

        public GamePlayer(string name)
        {
            this._name = name;
        }
    }
}
