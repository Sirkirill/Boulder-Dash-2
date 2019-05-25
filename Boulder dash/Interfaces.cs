using System;

namespace Boulderdash
{
    interface ISelfMovable
    {
        void ChooseDirection(Map map);
    }

    interface ICanInteract
    {
        void Hit(Map map);
    }

}