function ChangePro(a,b,c,d)
{
    pps = $("#Properties").val();
    pp1 = document.getElementById(b).value;
    pp1_1 = pp1.split("|");
    pp2_1 = pp1_1[0];
    pp2_2 = pp1_1[1];
    pp3_1 = pp2_2.split(",");
   
    $("#Properties").val(CheckProperties(pps,c,d));

    for (j = 0; j < pp3_1.length; j++)
    {
        b = 'ppid_' + pp3_1[j]
        if (a == b)
        {
            document.getElementById(b).className = "tb-selected";
        }
        else
        {
            document.getElementById(b).className = "notd";
        }
    }
    
}
function CheckProperties(e,f,g)
{
    var kk = e.split(",")
    var gg="";
    for (i =0; i < kk.length;i++)
    {
        if (i == 0)
        {
            if ((i + 1) == g) {

                gg =  f;
            }
            else {
                gg =kk[i];
            }
        }
        else
        {
            if ((i + 1) == g) {

                gg = gg + "," + f;
            }
            else {
                gg = gg + "," + kk[i];
            }
        }
        
    }
    return gg;
}