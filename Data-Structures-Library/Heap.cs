﻿using System;
using System.Collections.Generic;

namespace Data_Structures_Library
{
    /// <summary>
    /// Generic implementation of Binary (Min/Max) Heap Data Structure.
    /// </summary>
    /// <typeparam name="T">Type of the elements stored in the Heap.</typeparam>
    public class Heap<T>
    {
        // Number of elements in the Heap
        private int counter;
        // Elements of the Heap
        private T[] elements;
        // Boolean used for checking if the current Heap is Min or Max Heap
        private bool isMinHeap;
        // Variable used for setting the capacity of the Heap, if it is not provided
        private const int CAPACITY = 32;
        // Comparer object used to compare two objects of type T in the array
        Comparer<T> comparer = Comparer<T>.Default;

        /// <summary>
        /// Constructor for the Heap, using only one argument.
        /// </summary>
        /// <param name="isMin">Boolean determining whether Heap is Min or Max Heap.</param>
        public Heap(bool isMin = true)
        {
            elements = new T[CAPACITY];
            isMinHeap = isMin;
            counter = 0;
        }

        /// <summary>
        /// Constructor for the Heap, using two arguments.
        /// </summary>
        /// <param name="array">Array of object values, used to prefill the Heap.</param>
        /// <param name="isMin">Boolean determining whether Heap is Min or Max Heap.</param>
        public Heap(T[] array, bool isMin = true)
        {
            elements = new T[array.Length];
            Array.Copy(array, elements, array.Length);
            isMinHeap = isMin;
            counter = array.Length;

            //BuildHeap();
        }

        /// <summary>
        /// Helper method used for constructing the Heap Data Structure when the constructor is called.
        /// </summary>
        private void BuildHeap()
        {
            for(int i = counter / 2; i >= 0; i++)
            {
                Heapify(i);
            }
        }
        
        /// <summary>
        /// Helper method that forms and keeps the structure of the Heap.
        /// </summary>
        /// <param name="parentIndex"></param>
        private void Heapify(int parentIndex)
        {
            // Initialize the index of the child element
            int childIndex = -1;
            // Initialize the indices of left and right child element with the two formulas
            int leftChild = 2 * parentIndex + 1;
            int rightChild = leftChild + 1;

            // Check where the element should be
            if (leftChild < counter)
            {
                childIndex = leftChild;
            }

            if (rightChild < counter && (comparer.Compare(elements[leftChild], elements[rightChild]) < 0))
            {
                childIndex = rightChild;
            }

            // Swap the values and use recursion to put the next element in the right place in Heap
            if(childIndex != -1 && (comparer.Compare(elements[parentIndex], elements[childIndex]) < 0))
            {
                // Swap the values
                T temp = elements[parentIndex];
                elements[parentIndex] = elements[childIndex];
                elements[childIndex] = temp;
                // Use recursion to Heapify the elements below
                Heapify(childIndex);
            }
        }

        /// <summary>
        /// Getter for the number of elements in the heap.
        /// </summary>
        public int Length
        {
            get
            {
                return counter;
            }
        }

        /// <summary>
        /// Method that returns the first element of the Heap ("root").
        /// </summary>
        /// <returns>Element of the heap at index 0, if it exists, and the default value of type T, if the Heap is empty.</returns>
        public T Peek()
        {
            // Check if the Heap is empty
            if(counter == 0)
            {
                // If it is, return the default value for type T
                return default(T);
            }
            // Else, return the element at index 0
            return elements[0];
        }

        /// <summary>
        /// Method that prints out the values of all the elements in the Heap in the console.
        /// </summary>
        public void Print()
        {
            // Iterate through the array of elements and print out each value
            foreach(object element in elements)
            {
                Console.Write($"{element} ");
            }
        }

        /// <summary>
        /// Method that removes the element at the top of the Heap.
        /// </summary>
        /// <returns>Value of the deleted element.</returns>
        /// <exception cref="System.InvalidOperationException">Thrown when an attempt to delete from an empty Heap is made.</exception>
        public T Pop()
        {
            // Check if the Heap is empty, if it is, throw an exception
            if(counter == 0)
            {
                throw new InvalidOperationException("Error! Can't delete from an empty Heap.");
            }
            // Save the value of the element that will be deleted in a temporary variable
            T temp = elements[0];
            // Remove the element and decrease counter, by setting the first element at the index that will be out of the bounds of the array
            elements[0] = elements[counter - 1];
            counter--;
            // Return saved value
            return temp;
        }

        /// <summary>
        /// Method that adds the element at the end of the Heap.
        /// </summary>
        /// <param name="value">Value of the element that will be added.</param>
        public void Append(T value)
        {
            // Check if the Heap is full, and if it is, double it's size
            if(counter == elements.Length)
            {
                DoubleSize();
            }

            // Add the new element and increase the number of elements in the Heap
            elements[counter++] = value;

            // Rebuild the Heap
            BuildHeap();
        }

        /// <summary>
        /// Helper method that doubles the Heap size.
        /// </summary>
        private void DoubleSize()
        {
            // Copy the old array
            T[] old = elements;
            // Initialize the new array with double the size
            elements = new T[elements.Length * 2];
            // Copy the values from old array into the new one
            Array.Copy(old, 0, elements, 0, counter);
        }
    }
}
