public abstract class BaseState     //����������
{
    protected Enemy currentEnemy;       //��ȡ��ǰ������˭�������������������
    public abstract void OnEnter(Enemy enemy);     //״̬���Ľ���,����ʱ��õ�ǰenemy��˭
    public abstract void LogicUpdate();         //���е�boolֵ���ж϶����������
    public abstract void PhysicsUpdate();           //���к������߼��йص��ж϶�������
    public abstract void OnExit();          //״̬���˳�
}