using System;
using System.Linq;
using System.Runtime.Remoting.Messaging;

namespace Fishki
{
    public class BoardState : IEquatable<BoardState>
    {
        private readonly FishkaEnum[] _fishki;
        private readonly int _boardSize = 6;

        public static BoardState Create(bool isFinalState = false)
        {
            return new BoardState(isFinalState);
        }
        
        public static BoardState Create(BoardState prevousState, int rollFishkaNumber)
        {
            return new BoardState(prevousState, rollFishkaNumber);
        }
        
        private BoardState(bool isFinalState)
        {
            var fishka = isFinalState ? FishkaEnum.Disabled : FishkaEnum.Enabled;
            
            _fishki = new[]
            {
                fishka, //1
                fishka, //2
                fishka, //3
                fishka, //4
                fishka, //5 
                fishka //6
            };
        }

        private BoardState()
        {
            _fishki = new[]
            {
                FishkaEnum.Enabled, //1
                FishkaEnum.Enabled, //2
                FishkaEnum.Enabled, //3
                FishkaEnum.Enabled, //4
                FishkaEnum.Enabled, //5 
                FishkaEnum.Enabled //6
            };
        }

        private BoardState(BoardState prevousState, int rollFishkaNumber)
        {
            _fishki = (FishkaEnum[])prevousState._fishki.Clone();

            switch (rollFishkaNumber)
            {
                case 1:
                    Roll1();
                    break;
                case 2:
                    Roll2();
                    break;
                case 3:
                    Roll3();
                    break;
                case 4:
                    Roll4();
                    break;
                case 5:
                    Roll5();
                    break;
                case 6:
                    Roll6();
                    break;
            }
        }

        private void Roll6()
        {
            Roll(6);
            Roll(3);
            Roll(4);
        }

        private void Roll(int fishkaNumber)
        {
            _fishki[fishkaNumber - 1] = _fishki[fishkaNumber - 1].Roll();
        }

        private void Roll5()
        {
            Roll(5);
            Roll(1);
            Roll(2);
            Roll(3);
            Roll(4);
        }

        private void Roll4()
        {
            Roll(4);
            Roll(2);
            Roll(3);
            Roll(5);
            Roll(6);
        }

        private void Roll3()
        {
            Roll(3);
            Roll(1);
            Roll(4);
            Roll(5);
        }

        private void Roll2()
        {
            Roll(2);
            Roll(4);
            Roll(5);
            Roll(6);
        }

        private void Roll1()
        {
            Roll(1);
            Roll(3);
            Roll(4);
        }

        public bool Equals(BoardState other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;

            if (this._boardSize != other._boardSize) return false;

            for (int counter = 0; counter < _boardSize; counter++)
            {
                if (this._fishki[counter] != other._fishki[counter])
                {
                    return false;
                }
            }
            return true;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((BoardState)obj);
        }

        public override int GetHashCode()
        {
            return (_fishki != null ? _fishki.GetHashCode() : 0);
        }

        public override string ToString()
        {
            return $"State=[{_fishki.Aggregate(string.Empty, (s, @enum) => s + (int)@enum)}]";
        }
    }
}