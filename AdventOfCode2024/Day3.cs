using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2024
{
    internal static class Day3
    {
        internal static int Star1()
        {
            var sm = StateMachine.Build(false);
            using (var reader = new StreamReader("input/input3_1.txt"))
            {
                int ch;
                while ((ch = reader.Read()) != -1)
                {
                    sm.Process((char)ch);
                }
            }
            return sm.Aggregate;
        }

        internal static int Star2()
        {
            var sm = StateMachine.Build(true);
            using (var reader = new StreamReader("input/input3_1.txt"))
            {
                int ch;
                while ((ch = reader.Read()) != -1)
                {
                    sm.Process((char)ch);
                }
            }
            return sm.Aggregate;
        }

        private class StateMachine()
        {
            private State Initial;
            private State Current;
            private State Final;

            internal string first = string.Empty, second = string.Empty;
            internal int Aggregate;

            public bool Enabled => enabled || !changeState;
            private bool enabled = true;
            private bool changeState = false;

            private void SetCurrent(State newState, char input)
            {
                this.Current = newState;
                newState.Output?.Invoke(input);
            }

            public static StateMachine Build(bool changeState)
            {
                var sm = new StateMachine();

                var doRB = new State(c => c == ')', (c) =>
                {
                    sm.enabled = true;
                    sm.SetCurrent(sm.Initial, c);
                }, Array.Empty<State>());
                var doLB = new State(c => c == '(', null, doRB);

                var dontRB = new State(c => c == ')', (c) =>
                {
                    sm.enabled = false;
                    sm.SetCurrent(sm.Initial, c);
                }, Array.Empty<State>());
                var dontLB = new State(c => c == '(', null, dontRB);

                var t = new State(c => c == 't', null, dontLB);
                var quote = new State(c => c == '\'', null, t);
                var n = new State(c => c == 'n', null, quote);
                var o = new State(c => c == 'o', null, n, doLB);
                var d = new State(c => c == 'd', null, o);

                var end = new State(c => c == ')', (c) =>
                {
                    sm.Aggregate += int.Parse(sm.first) * int.Parse(sm.second);
                    sm.SetCurrent(sm.Initial, c);
                }, Array.Empty<State>());

                var sN3 = new State(c => c >= '0' && c <= '9', c => sm.second += c, end);
                var sN2 = new State(c => c >= '0' && c <= '9', c => sm.second += c, end, sN3);
                var sN1 = new State(c => c >= '0' && c <= '9', c => sm.second += c, end, sN2, sN3);
                var com = new State(c => c == ',', null, sN1);
                var fN3 = new State(c => c >= '0' && c <= '9', c => sm.first += c, com);
                var fN2 = new State(c => c >= '0' && c <= '9', c => sm.first += c, com, fN3);
                var fN1 = new State(c => c >= '0' && c <= '9', c => sm.first += c, com, fN2, fN3);
                var lbr = new State(c => c == '(', null, fN1);
                var lSt = new State(c => c == 'l', null, lbr);
                var uSt = new State(c => c == 'u', null, lSt);
                var mSt = new State(c => sm.Enabled && c == 'm', null, uSt);
                var str = new State(c => true, (c) =>
                {
                    sm.first = string.Empty;
                    sm.second = string.Empty;
                }, mSt, d);

                sm.changeState = changeState;
                sm.Initial = str;
                sm.Current = str;
                sm.Final = end;

                return sm;
            }

            public void Process(char c)
            {
                SetCurrent(Current.NextStates.FirstOrDefault(x => x.Condition(c)) ?? Initial, c);
            }

            private class State
            {
                public State(Predicate<char> condition, Action<char> output, params State[] states)
                {
                    Condition = condition;
                    Output = output;
                    NextStates = states;
                }

                internal Predicate<char> Condition;
                internal Action<char> Output;
                internal State[] NextStates;
            }
        }
    }
}
