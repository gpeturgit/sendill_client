using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sendil_mobile_web
{
    public partial class ledgerlist : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<ledger> llist = new List<ledger>();
            ledger iledger = new ledger();
            iledger.id = 1;
            iledger.name = "Janúar - Ásmundur";
            iledger.date = "01.01.2012";
            llist.Add(iledger);
            iledger = new ledger();
            iledger.id = 2;
            iledger.name = "Janúar - Pétur";
            iledger.date = "05.01.2012";
            llist.Add(iledger);

            ledger_list.DataSource = llist;
            ledger_list.DataBind();
        }
    }
}