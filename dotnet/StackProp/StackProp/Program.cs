//// See https://aka.ms/new-console-template for more information
//Console.WriteLine("Hello, World!");


using System;

class StackProp
{
    string[] stack = new string[10];
    int top = -1;

    public void Push(string action)
    {
        top++;
        stack[top] = action;
        Show();
    }

    public void Pop()
    {
        if (top == -1)
        {
            Console.WriteLine("Nothing here");
        }
        else
        {
            top--;
        }
        Show();
    }

    public void Show()
    {
        Console.Write("Present Prop: ");

        if (top == -1)
        {
            Console.WriteLine("Zero");
            return;
        }

        for (int i = 0; i <= top; i++)
        {
            Console.Write(stack[i] + " ");
        }

        Console.WriteLine();
    }
}

class Program
{
    static void Main()
    {
        StackProp edit = new StackProp();

        edit.Push("First");
        edit.Push("Second");
        edit.Push("Third");
        //edit.Push("First");
        //edit.Push("Second");
        //edit.Push("Third");
        //edit.Push("First");
        //edit.Push("Second");
        //edit.Push("Third");
        //edit.Push("First");
        //edit.Push("Second");
        //edit.Push("Third");

        edit.Pop();
        edit.Pop();
    }
}
