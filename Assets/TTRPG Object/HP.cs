
public class TTRPG_HP
{
    int maxHP;
    int currHP;
    int tmpHP;

    public TTRPG_HP(int maxHP, int currHP, int tmpHP)
    {
        this.maxHP = maxHP;
        this.currHP = currHP;
        this.tmpHP = tmpHP;
    }

    public int getMaxHP() { return maxHP; }
    public int getCurrHP() { return currHP; }
    public int getTmpHP() { return tmpHP; }

    public void setMaxHP(int maxHP) { this.maxHP = maxHP; }
    public void setCurrHP(int currHP) { this.currHP = currHP; }
    public void setTmpHP(int tmpHP) { this.tmpHP = tmpHP; }

}

