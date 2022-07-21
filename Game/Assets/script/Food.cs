using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : Eatable
{
    public override void OnDigest()
    {
        GameManager.Instance.score++;
        base.OnDigest();
    }
}
