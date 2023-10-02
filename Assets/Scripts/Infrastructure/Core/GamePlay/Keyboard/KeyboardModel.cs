using System;
using System.Collections.Generic;

public class KeyboardModel
{
    public List<KeyPresenter> Keys { get; private set; }
    
    public GameTaskStaticData TaskStaticData { get; set; }
    
    public void SetKeys(List<KeyPresenter> keys)
    {
        Keys = keys;
    }
    
}