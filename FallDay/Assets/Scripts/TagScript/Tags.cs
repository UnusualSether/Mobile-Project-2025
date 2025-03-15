
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tags : MonoBehaviour
{
    [SerializeField]
    private List<Tag> _tags;

    public List<Tag> All => _tags;

    public bool HasTag(Tag t) {  return _tags.Contains(t); }

    public bool HasTag(string tagName)
    {
        return _tags.Exists(t => string.Equals(t.Name, tagName, System.StringComparison.InvariantCultureIgnoreCase));
    }
}
