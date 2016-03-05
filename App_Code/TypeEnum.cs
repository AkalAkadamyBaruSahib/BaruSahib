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

    public enum TransportDocumentType : int
    {
        Registration = 1,
        Pollution = 2,
        Permit = 3,
        Tax = 4,
        Passing = 5,
        Insurance = 6,
        WrittenContract = 7
    }

    public enum UserType : int
    {
        ADMIN = 1,
        CONSTRUCTION = 2,
        AUDIT = 3,
        PURCHASE = 4,
        ACCOUNT = 5,
        WORKSHOP = 6,
        ARCHITECTURAL = 7,
        MAINTANENCE = 8,
        STORE = 9,
        ACADEMIC = 10,
        HR = 11,
        PURCHASEEMPLOYEE = 12,
        TRANSPORTADMIN = 13,
        TRANSPORTMANAGER = 14,
        BACKOFFICE = 15,
        INSURANCECOORDINATOR = 16,
        TRANSPORTINCHARGE = 17,
        BACKOFFICEHO = 18,
        TRANSPORTTRAINEE = 19,
        BACKOFFICETRAINEE = 20,
        CONSTRUCTIONSUBADMIN = 21,
        FrontDesk = 22
    }
}