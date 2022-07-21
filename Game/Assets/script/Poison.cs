using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poison : Eatable
{
    public override void OnDigest()
    {
        GameManager.Instance.RemoveScore();
        base.OnDigest();
    }
}
