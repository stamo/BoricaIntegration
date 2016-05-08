using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Borica.Integration.Models
{
    /// <summary>
    /// Author: Stamo Petkov
    /// Created: 07.05.2016
    /// Description: Parsed Borica responce data
    /// </summary>
    public class BoricaResponceModel
    {
        // In result of the transaction request, the merchant’s site receives from APGW a BOResp message. 
        // The format of BOResp is as follows:

        // No.	Field name							Format
        // 1	Type of the performed transaction	N[2]
        // 2	Date/Time							YYYYMMDDHHMISS
        // 3	Transaction amount					N[12]
        // 4	Terminal identifier					A[8]
        // 5	Number of the order at the merchant	A[15]
        // 6	Finalization code					N[2]
        // 7	Protocol version					A[3]
        // 8	Digital signature					B[128]
    }
}
