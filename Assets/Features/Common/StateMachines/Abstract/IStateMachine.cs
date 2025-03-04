﻿using Internal;

namespace Features.Common.StateMachines.Abstract
{
    public interface IStateMachine
    {
        IViewableDelegate Exited { get; }

        bool IsAvailable(IState state);
        IStateHandle CreateHandle(IState state);
        void Exit(IState state);
    }
}