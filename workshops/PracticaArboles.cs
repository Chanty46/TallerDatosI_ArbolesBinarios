using System;
using System.Collections.Generic;

/*
Instituto Tecnologico de Costa Rica - Ingenieria en Computadores
Practica: Catalogo de arboles binarios | Parejas | 2 clases de 2 horas
Todo el trabajo se realiza durante las clases. Entregue el catalogo y este archivo
al comenzar las exposiciones (minuto 75 de la segunda clase).
Cada pareja expone un arbol ante el grupo: turno de 3 minutos con transicion.
Consulte en la guia la asignacion del arbol y el contenido de la exposicion.
Complete los 8 TODO. Conserve las firmas y utilice los auxiliares.
Los ejemplos se ejecutan por separado: un TODO pendiente no detiene los demas.
Se ignoran claves duplicadas en los arboles de busqueda; Heap admite duplicados.
En la impresion: L = hijo izquierdo, R = hijo derecho.
La guia Practica_Catalogo_Arboles.md contiene los ejercicios y el catalogo.
*/

public class Node
{
    public int Value;
    public Node Left, Right;
    public int Height = 1;
    public Node(int value) { Value = value; }
}

public static class View
{
    public static void Print(Node n, string path = "Raiz")
    {
        if (n == null) return;
        Console.WriteLine(path + ": " + n.Value);
        Print(n.Left, path + ".L"); Print(n.Right, path + ".R");
    }
    public static string InOrder(Node n)
    {
        List<int> values = new List<int>();
        Collect(n, values);
        return string.Join(" ", values);
    }
    private static void Collect(Node n, List<int> values)
    {
        if (n == null) return;
        Collect(n.Left, values); values.Add(n.Value); Collect(n.Right, values);
    }
}

public class BST
{
    public Node Root;
    public void Insert(int value) { Root = Insert(Root, value); }
    private Node Insert(Node n, int value)
    {
        if (n == null) return new Node(value);
        if (value < n.Value) n.Left = Insert(n.Left, value);
        else if (value > n.Value) n.Right = Insert(n.Right, value);
        return n;
    }
    public void Remove(int value) { Root = Remove(Root, value); }
    private Node Minimum(Node n)
    {
        while (n.Left != null) n = n.Left;
        return n;
    }
    private Node Remove(Node n, int value)
    {
        // TODO 1: devuelva la nueva raiz del subarbol.
        // Contemple null, hoja, un hijo y dos hijos. Utilice Minimum(n.Right).
        throw new NotImplementedException("BST.Remove");
    }
}

public class AVL
{
    public Node Root;
    public int Height(Node n) { return n == null ? 0 : n.Height; }
    private void UpdateHeight(Node n)
    {
        n.Height = 1 + Math.Max(Height(n.Left), Height(n.Right));
    }
    public int Balance(Node n) { return n == null ? 0 : Height(n.Left) - Height(n.Right); }
    private Node RotateLeft(Node n)
    {
        // TODO 2: conserve el subarbol intermedio; actualice primero n,
        // luego la nueva raiz. Precondicion: n.Right != null.
        throw new NotImplementedException("AVL.RotateLeft");
    }
    private Node RotateRight(Node n)
    {
        // TODO 3: operacion simetrica. Precondicion: n.Left != null.
        throw new NotImplementedException("AVL.RotateRight");
    }
    public void Insert(int value) { Root = Insert(Root, value); }
    private Node Insert(Node n, int value)
    {
        if (n == null) return new Node(value);
        if (value < n.Value) n.Left = Insert(n.Left, value);
        else if (value > n.Value) n.Right = Insert(n.Right, value);
        else return n;
        UpdateHeight(n);
        int balance = Balance(n);
        if (balance > 1)
        {
            if (value > n.Left.Value) n.Left = RotateLeft(n.Left);
            return RotateRight(n);
        }
        if (balance < -1)
        {
            if (value < n.Right.Value) n.Right = RotateRight(n.Right);
            return RotateLeft(n);
        }
        return n;
    }
}

public class RBNode
{
    public int Value;
    public bool Red = true;
    public RBNode Left, Right, Parent;
    public RBNode(int value) { Value = value; }
}

public class RedBlackTree
{
    public RBNode Root;
    private bool IsRed(RBNode n) { return n != null && n.Red; }
    // null representa hojas NIL negras. No se insertan claves duplicadas.
    public void Insert(int value)
    {
        RBNode parent = null, current = Root;
        while (current != null)
        {
            parent = current;
            if (value == current.Value) return;
            current = value < current.Value ? current.Left : current.Right;
        }
        RBNode n = new RBNode(value); n.Parent = parent;
        if (parent == null) Root = n;
        else if (value < parent.Value) parent.Left = n;
        else parent.Right = n;
        FixInsert(n);
    }
    private RBNode RepairRedUncle(RBNode n)
    {
        // TODO 4: padre y tio son rojos; abuelo existe.
        // Recoloree padre, tio y abuelo. Devuelva el abuelo para continuar
        // la reparacion arriba. FixInsert pintara de negro la raiz al final.
        throw new NotImplementedException("RedBlackTree.RepairRedUncle");
    }
    private void FixInsert(RBNode n)
    {
        while (n.Parent != null && n.Parent.Red)
        {
            RBNode p = n.Parent, g = p.Parent;
            bool parentLeft = p == g.Left;
            RBNode uncle = parentLeft ? g.Right : g.Left;
            if (IsRed(uncle)) { n = RepairRedUncle(n); continue; }
            if (parentLeft)
            {
                if (n == p.Right) { n = p; RotateLeft(n); }
                n.Parent.Red = false;
                n.Parent.Parent.Red = true;
                RotateRight(n.Parent.Parent);
            }
            else
            {
                if (n == p.Left) { n = p; RotateRight(n); }
                n.Parent.Red = false;
                n.Parent.Parent.Red = true;
                RotateLeft(n.Parent.Parent);
            }
        }
        Root.Red = false;
    }
    private void RotateLeft(RBNode n)
    {
        RBNode next = n.Right;
        n.Right = next.Left;
        if (next.Left != null) next.Left.Parent = n;
        next.Parent = n.Parent;
        if (n.Parent == null) Root = next;
        else if (n == n.Parent.Left) n.Parent.Left = next;
        else n.Parent.Right = next;
        next.Left = n; n.Parent = next;
    }
    private void RotateRight(RBNode n)
    {
        RBNode next = n.Left;
        n.Left = next.Right;
        if (next.Right != null) next.Right.Parent = n;
        next.Parent = n.Parent;
        if (n.Parent == null) Root = next;
        else if (n == n.Parent.Left) n.Parent.Left = next;
        else n.Parent.Right = next;
        next.Right = n; n.Parent = next;
    }
    public void Print() { Print(Root, "Raiz"); }
    private void Print(RBNode n, string path)
    {
        if (n == null) return;
        Console.WriteLine(path + ": " + n.Value + (n.Red ? " (R)" : " (B)"));
        Print(n.Left, path + ".L"); Print(n.Right, path + ".R");
    }
}

public class SplayTree
{
    public Node Root;
    private Node RotateLeft(Node n)
    {
        Node next = n.Right;
        n.Right = next.Left; next.Left = n;
        return next;
    }
    private Node RotateRight(Node n)
    {
        Node next = n.Left;
        n.Left = next.Right; next.Right = n;
        return next;
    }
    private Node ZigZig(Node n, bool left)
    {
        // TODO 5: dos rotaciones del mismo sentido sobre la raiz local.
        // left=true: camino izquierda-izquierda; false: derecha-derecha.
        // Los dos enlaces necesarios existen. Devuelva la nueva raiz.
        throw new NotImplementedException("SplayTree.ZigZig");
    }
    private Node ZigZag(Node n, bool left)
    {
        // TODO 6: left=true: camino izquierda-derecha; false: derecha-izquierda.
        // Rote primero el hijo, reasignelo y rote la raiz local.
        // Los dos enlaces necesarios existen. Devuelva la nueva raiz.
        throw new NotImplementedException("SplayTree.ZigZag");
    }
    private Node Splay(Node n, int value)
    {
        if (n == null || n.Value == value) return n;
        if (value < n.Value)
        {
            if (n.Left == null) return n;
            if (value < n.Left.Value)
            {
                n.Left.Left = Splay(n.Left.Left, value);
                if (n.Left.Left != null) return ZigZig(n, true);
            }
            else if (value > n.Left.Value)
            {
                n.Left.Right = Splay(n.Left.Right, value);
                if (n.Left.Right != null) return ZigZag(n, true);
            }
            return RotateRight(n);
        }
        if (n.Right == null) return n;
        if (value > n.Right.Value)
        {
            n.Right.Right = Splay(n.Right.Right, value);
            if (n.Right.Right != null) return ZigZig(n, false);
        }
        else if (value < n.Right.Value)
        {
            n.Right.Left = Splay(n.Right.Left, value);
            if (n.Right.Left != null) return ZigZag(n, false);
        }
        return RotateLeft(n);
    }
    public bool Search(int value)
    {
        Root = Splay(Root, value);
        return Root != null && Root.Value == value;
    }
    public void Insert(int value)
    {
        if (Root == null) { Root = new Node(value); return; }
        Root = Splay(Root, value);
        if (Root.Value == value) return;
        Node n = new Node(value);
        if (value < Root.Value)
        {
            n.Left = Root.Left; n.Right = Root; Root.Left = null;
        }
        else
        {
            n.Right = Root.Right; n.Left = Root; Root.Right = null;
        }
        Root = n;
    }
}

public class MaxHeap
{
    private List<int> data = new List<int>();
    public void Insert(int value)
    {
        data.Add(value);
        int i = data.Count - 1;
        while (i > 0)
        {
            int parent = (i - 1) / 2;
            if (data[parent] >= data[i]) break;
            Swap(parent, i); i = parent;
        }
    }
    private void Swap(int a, int b)
    {
        int temp = data[a]; data[a] = data[b]; data[b] = temp;
    }
    private void SiftDown(int i)
    {
        // TODO 7: compare con ambos hijos; intercambie con el mayor
        // si supera al padre; continue hasta restaurar el max heap.
        // Debe funcionar con cero hijos o solo un hijo izquierdo.
        throw new NotImplementedException("MaxHeap.SiftDown");
    }
    public int ExtractMax()
    {
        if (data.Count == 0) throw new InvalidOperationException("Heap vacio");
        int max = data[0];
        data[0] = data[data.Count - 1]; data.RemoveAt(data.Count - 1);
        if (data.Count > 0) SiftDown(0);
        return max;
    }
    public string AsArray() { return string.Join(" ", data); }
    public void Print() { Print(0, "Raiz"); }
    private void Print(int i, string path)
    {
        if (i >= data.Count) return;
        Console.WriteLine(path + ": " + data[i]);
        Print(2 * i + 1, path + ".L"); Print(2 * i + 2, path + ".R");
    }
    public bool IsValid()
    {
        // TODO 8: recorra el arreglo y verifique padre >= cada hijo existente.
        // El heap vacio y el de un elemento son validos.
        throw new NotImplementedException("MaxHeap.IsValid");
    }
}

public class Program
{
    private static void Run(string title, Action example)
    {
        Console.WriteLine("\n--- " + title + " ---");
        try { example(); }
        catch (NotImplementedException e) { Console.WriteLine("PENDIENTE: " + e.Message); }
        catch (Exception e) { Console.WriteLine("REVISAR: " + e.GetType().Name + " - " + e.Message); }
    }
    private static void Check(string expected, string actual)
    {
        Console.WriteLine("Esperado: " + expected);
        Console.WriteLine("Obtenido: " + actual);
        Console.WriteLine(expected == actual ? "OK" : "REVISAR");
    }
    public static void Main()
    {
        int[] values = { 50, 30, 70, 20, 40, 60, 80, 35 };
        foreach (int removed in new int[] { 20, 40, 50, 999 })
        {
            int target = removed;
            Run("BST eliminar " + target, delegate {
                BST t = new BST(); foreach (int v in values) t.Insert(v);
                t.Remove(target); View.Print(t.Root);
                string expected = target == 20 ? "30 35 40 50 60 70 80" :
                    target == 40 ? "20 30 35 50 60 70 80" :
                    target == 50 ? "20 30 35 40 60 70 80" : "20 30 35 40 50 60 70 80";
                Check(expected, View.InOrder(t.Root));
                if (target == 50) Check("60", t.Root.Value.ToString());
            });
        }
        Run("BST vacio y raiz unica", delegate {
            BST t = new BST(); t.Remove(1); t.Insert(1); t.Remove(1);
            Check("", View.InOrder(t.Root));
        });
        int[][] sequences = { new int[] { 30, 20, 10 }, new int[] { 10, 20, 30 },
            new int[] { 30, 10, 20 }, new int[] { 10, 30, 20 } };
        string[] names = { "LL", "RR", "LR", "RL" };
        for (int i = 0; i < sequences.Length; i++)
        {
            int index = i;
            Run("AVL " + names[index], delegate {
                AVL t = new AVL(); foreach (int v in sequences[index]) t.Insert(v);
                View.Print(t.Root); Check("10 20 30", View.InOrder(t.Root));
                Check("20", t.Root.Value.ToString()); Check("2", t.Root.Height.ToString());
            });
        }
        Run("AVL conserva subarboles", delegate {
            AVL t = new AVL(); foreach (int v in new int[] { 30, 20, 40, 10, 25, 5 }) t.Insert(v);
            View.Print(t.Root); Check("5 10 20 25 30 40", View.InOrder(t.Root));
            Check("20", t.Root.Value.ToString());
        });
        Run("Red Black padre y tio rojos", delegate {
            RedBlackTree t = new RedBlackTree();
            foreach (int v in new int[] { 10, 5, 15, 1 }) t.Insert(v);
            t.Print(); Check("False False False True", t.Root.Red + " " +
                t.Root.Left.Red + " " + t.Root.Right.Red + " " + t.Root.Left.Left.Red);
        });
        Run("Red Black reparacion hacia arriba", delegate {
            RedBlackTree t = new RedBlackTree();
            foreach (int v in new int[] { 10, 5, 15, 1, 6, 12, 18, 0, -1, -2 }) t.Insert(v);
            t.Print(); Console.WriteLine("Verifique manualmente todas las reglas, incluyendo NIL.");
        });
        for (int i = 0; i < 4; i++)
        {
            int index = i;
            Run("Splay " + names[index], delegate {
                // Arboles iniciales fijos para aislar los cuatro casos.
                SplayTree t = new SplayTree();
                int target;
                if (index == 0) { t.Root = new Node(30); t.Root.Left = new Node(20); t.Root.Left.Left = new Node(10); target = 10; }
                else if (index == 1) { t.Root = new Node(10); t.Root.Right = new Node(20); t.Root.Right.Right = new Node(30); target = 30; }
                else if (index == 2) { t.Root = new Node(30); t.Root.Left = new Node(10); t.Root.Left.Right = new Node(20); target = 20; }
                else { t.Root = new Node(10); t.Root.Right = new Node(30); t.Root.Right.Left = new Node(20); target = 20; }
                Check("True", t.Search(target).ToString());
                Check(target.ToString(), t.Root.Value.ToString());
                Check("10 20 30", View.InOrder(t.Root)); View.Print(t.Root);
                Check("True", t.Search(target).ToString());
                Check("False", t.Search(999).ToString());
                Check("10 20 30", View.InOrder(t.Root));
            });
        }
        Run("Heap extracciones", delegate {
            MaxHeap h = new MaxHeap();
            foreach (int v in new int[] { 50, 30, 70, 20, 40, 60, 80 }) h.Insert(v);
            Console.WriteLine("Inicial: " + h.AsArray()); h.Print();
            Check("True", h.IsValid().ToString());
            List<int> extracted = new List<int>();
            for (int i = 0; i < 7; i++)
            {
                extracted.Add(h.ExtractMax()); Console.WriteLine("Arreglo: " + h.AsArray());
                Check("True", h.IsValid().ToString());
            }
            Check("80 70 60 50 40 30 20", string.Join(" ", extracted));
            try { h.ExtractMax(); Console.WriteLine("REVISAR: faltaba excepcion"); }
            catch (InvalidOperationException) { Console.WriteLine("OK: heap vacio detectado"); }
        });
        Run("Heap duplicados y un hijo", delegate {
            MaxHeap h = new MaxHeap(); foreach (int v in new int[] { 9, 9, 4 }) h.Insert(v);
            Check("9", h.ExtractMax().ToString()); Check("True", h.IsValid().ToString());
            Check("9", h.ExtractMax().ToString()); Check("4", h.ExtractMax().ToString());
            Check("True", h.IsValid().ToString());
        });
    }
}
