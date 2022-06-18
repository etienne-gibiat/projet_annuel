namespace _Elementis.Scripts
{
    public interface IPuzzlePiece
    {
        void SetPuzzle(InterruptorPuzzle interruptorPuzzle);
        bool IsActivated { get; }
    }
}