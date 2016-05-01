using System.Collections.Generic;
using UnityEngine;
public class ResourceContainer
{
    public Dictionary<string, int> Resources = new Dictionary<string, int>();
    
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
    
    public int Consume(Resource _resource, int _amount)
    {
        int _amountGot = 0;
        
        if(Resources.ContainsKey(_resource.Name))
        {
            if((Resources[_resource.Name] - _amount) > 0)
            {
                Resources[_resource.Name] -= _amount;
                _amountGot = _amount;
            }
            else
            {
                _amountGot = _amount - Mathf.Abs(Resources[_resource.Name] - _amount);
                Resources[_resource.Name] = 0;
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