﻿using System;

namespace WSCT.Wrapper.PCSCLite32
{
    internal sealed class ReaderState : AbstractReaderState
    {
        #region >> Properties

        public ScardReaderState ScReaderState;

        public override string ReaderName
        {
            get { return ScReaderState.readerName; }
            set { ScReaderState.readerName = value; }
        }

        public override EventState CurrentState
        {
            get { return (EventState)ScReaderState.currentState; }
            set { ScReaderState.currentState = (uint)value; }
        }

        public override EventState EventState
        {
            get { return (EventState)ScReaderState.eventState; }
            set { ScReaderState.eventState = (uint)value; }
        }

        public override byte[] Atr
        {
            get
            {
                if ((ScReaderState.atr != null) && (ScReaderState.atr.Length > ScReaderState.atrSize))
                {
                    Array.Resize(ref ScReaderState.atr, (int)ScReaderState.atrSize);
                }
                return ScReaderState.atr;
            }
            set { ScReaderState.atr = value; }
        }

        #endregion

        #region >> Constructors

        public ReaderState()
        {
            ScReaderState = new ScardReaderState();
        }

        public ReaderState(string readerName)
            : this()
        {
            ReaderName = readerName;
        }

        public ReaderState(string readerName, EventState currentState, EventState eventState)
            : this()
        {
            ReaderName = readerName;
            CurrentState = currentState;
            EventState = eventState;
        }

        #endregion
    }
}