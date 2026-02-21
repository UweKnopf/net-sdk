using System;

namespace net_sdk.src.internal_classes;

public abstract record class Model
{

    public required TCGDex TCGDex;

    

    /*
    public override string ToString()
    {
        return base.ToString();
    }

    public static T Build<T>(T model, object data, TCGDex tCGDex){
        tCGDex = tCGDex;
        return model;
    }
    */
    

    
}
