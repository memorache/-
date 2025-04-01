public abstract class BaseState     //基础抽象类
{
    protected Enemy currentEnemy;       //获取当前敌人是谁，用于让所有子类访问
    public abstract void OnEnter(Enemy enemy);     //状态机的进入,进入时获得当前enemy是谁
    public abstract void LogicUpdate();         //所有的bool值的判断都会放在这里
    public abstract void PhysicsUpdate();           //所有和物理逻辑有关的判断都在这里
    public abstract void OnExit();          //状态机退出
}