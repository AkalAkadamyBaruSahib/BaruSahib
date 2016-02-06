using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for TypeEnum
/// </summary>
public class TypeEnum
{
	public TypeEnum()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public enum TransportEmployeeType : int
    {
        FamilyMember = 1,
        Reference = 2
    }
}