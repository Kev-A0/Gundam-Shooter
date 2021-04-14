using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// This class used when formatting to JSON.
/// This class is mainly used when writing to the database.
/// 
/// Author: Kevin Lee
/// Data: April 12, 2021
/// Version 1.0
/// </summary>
class HighscoreUser
{
    public string name;
    public double points;

    public HighscoreUser()
    {

    }

    public HighscoreUser(string name, double points)
    {
        this.name = name;
        this.points = points;
    }
}

