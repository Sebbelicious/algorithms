namespace Mini_Project_3_Searching_Shakespeare
{
    public interface IChildNode : INode
    {
        //To be able to get start on both LinkedNode and KeyNode
        int Start { get; }
    }
}