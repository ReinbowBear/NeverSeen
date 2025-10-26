using System;

#region system
public class OnSave : EventArgs { }
public class OnLoad : EventArgs { }
#endregion

#region UI
public class OnChoise : EventArgs { }
#endregion

#region Scene
public class OnSceneStart : EventArgs { }
public class OnSceneRelease : EventArgs { }
#endregion

#region gamelay
public class OnNewEntity : EventArgs { }
public class OnUpdateNetwork : EventArgs { }
public class OnGameOver : EventArgs { }
#endregion
