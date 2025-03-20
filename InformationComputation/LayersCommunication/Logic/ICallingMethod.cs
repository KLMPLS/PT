﻿//__________________________________________________________________________________________
//
//  Copyright 2023 Mariusz Postol LODZ POLAND.
//
//  To be in touch join the community by pressing the `Watch` button and to get started
//  comment using the discussion panel at
//  https://github.com/mpostol/TP/discussions/182
//  with an introduction of yourself and tell us about what you do with this community.
//__________________________________________________________________________________________

namespace TP.InformationComputation.LayersCommunication.Logic
{
  public interface ICallingMethod : ILogic
  {
    /// <summary>
    ///Is called to check if the methods call sequence is correct.
    /// </summary>
    /// <returns></returns>
    bool CheckConsistency();
  }
}