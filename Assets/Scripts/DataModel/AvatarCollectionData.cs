using System;
using System.Collections.Generic;
using Unity.VisualScripting;

[Serializable, Inspectable]
public class AvatarCollectionData
{
    [Inspectable] public List<string> avatarArray = new ();
}