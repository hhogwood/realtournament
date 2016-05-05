using System.Collections.Generic;
using UnityEngine;

public class ResourceContainer : MonoBehaviour
{
    //dumb garbage to serialize the dictionary
    [System.Serializable()]
    public class SerializableDictionary : SerializableDictionaryBase<string, int>
    {

    }
    public SerializableDictionary Resources = new SerializableDictionary();
    
    public void Add(Resource _resource, int _amount)
    {
        Add(_resource.Name, _amount);
    }
    
    public void Add(string _name, int _amount)
    {
        
        if(Resources.ContainsKey(_name))
        {
            Resources[_name] += _amount;
        }
        else
        {
            Resources.Add(_name, _amount);
        }
        
        Mathf.Min(Resources[_name], 0);
    }
    
    public int Consume(string _resource, int _amount)
    {
        int _amountGot = 0;
        
        if(Resources.ContainsKey(_resource))
        {
            if((Resources[_resource] - _amount) > 0)
            {
                Resources[_resource] -= _amount;
                _amountGot = _amount;
            }
            else
            {
                _amountGot = _amount - Mathf.Abs(Resources[_resource] - _amount);
                Resources[_resource] = 0;
            }
        }
        
        return _amountGot;
    }
    
    public void Transfer(ResourceContainer _targetResourceContainer)
    {
        foreach(KeyValuePair<string,int> kvp in Resources)
        {
            _targetResourceContainer.Add(kvp.Key, kvp.Value);
        }
    }

    
}