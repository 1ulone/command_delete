using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class WallTypeData
{
    public static HashSet<int> wallTop = new HashSet<int>
    {
        0b1111,
        0b0110,
        0b0011,
        0b0010,
        0b1010,
        0b1100,
        0b1110,
        0b1011,
        0b0111
    };

    public static HashSet<int> wallSideLeft = new HashSet<int>
    {
        0b0100
    };

    public static HashSet<int> wallSideRight = new HashSet<int>
    {
        0b0001
    };

    public static HashSet<int> wallBottom = new HashSet<int>
    {
        0b1000
    };

    public static HashSet<int> wallCornerFacingUpRight = new HashSet<int>
    {
        0b10000011,
        0b11000111,
        0b10000111,
        0b11000011,
        0b11010111,
        0b10010011,
        0b11010011,
        0b10000010,
        0b10010010,
        0b11000110
    };

    public static HashSet<int> wallCornerFacingUpLeft = new HashSet<int>
    {
        0b11100000,
        0b11110001,
        0b11110000,
        0b11110100,
        0b11100001,
        0b11100100,
        0b11100101,
        0b10100000,
        0b10100100,
        0b10110001,
    };

    public static HashSet<int> wallCornerFacingDownRight = new HashSet<int> 
    {
        0b00011111,
        0b00001110,
        0b01001110,
        0b01001111,
        0b01011110,
        0b01011111,
        0b00011110,
        0b00001111,
        0b00001010,
        0b01001010,
        0b00011011
    };

    public static HashSet<int> wallCornerFacingDownLeft = new HashSet<int> 
    {
        0b00111000,
        0b00111001,
        0b00111100,
        0b00111101,
        0b01111001,
        0b00111100,
        0b01111101,
        0b01111100,
        0b01111001,
        0b01111000,
        0b00101000,
        0b00101001,
        0b01101101,
        0b01101100
    };

    public static HashSet<int> wallDiagonalCornerDownLeft = new HashSet<int>
    {
        0b01000000
    };

    public static HashSet<int> wallDiagonalCornerDownRight = new HashSet<int>
    {
        0b00000001
    };

    public static HashSet<int> wallDiagonalCornerUpLeft = new HashSet<int>
    {
        0b00010000,
        0b01010000,
    };

    public static HashSet<int> wallDiagonalCornerUpRight = new HashSet<int>
    {
        0b00000100,
        0b00000101
    };

    public static HashSet<int> wallHorizontalSideOne = new HashSet<int> 
    { 
        0b01110111, 
        0b00110110,
        0b01110110,
        0b00110100,
        0b01100011,
        0b01110011,
        0b01100111
    };

    public static HashSet<int> wallVerticalSideOne = new HashSet<int>
    { 
        0b11011101,
        0b10001101,
        0b11001101,
        0b10011101,
        0b11011000,
        0b11011100,
        0b11011001
    };

    public static HashSet<int> wallVerticalCornerOneLeft = new HashSet<int> { 0b11011111 };
    public static HashSet<int> wallVerticalCornerOneRight = new HashSet<int> { 0b11111101 };

    public static HashSet<int> wallHorizontalCornerOneUp = new HashSet<int> { 0b11110111 };
    public static HashSet<int> wallHorizontalCornerOneDown = new HashSet<int> { 0b01111111 };

    public static HashSet<int> wallFull = new HashSet<int>
    {
        0b1101,
        0b0101,
        0b1101,
        0b1001
    };

    public static HashSet<int> wallFullEightDirections = new HashSet<int>
    {
        0b00010100,
        0b11100100,
        0b10010011,
        0b01110100,
        0b00010111,
        0b00010110,
        0b00110100,
        0b00010101,
        0b01010100,
        0b00010010,
        0b00100100,
        0b00010011,
        0b01100100,
        0b10010111,
        0b11110100,
        0b10010110,
        0b10110100,
        0b11100101,
        0b11010011,
        0b11110101,
        0b11010111,
        0b11010111,
        0b11110101,
        0b01110101,
        0b01010111,
        0b01100101,
        0b01010011,
        0b01010010,
        0b00100101,
        0b00110101,
        0b01010110,
        0b11010101,
        0b11010100,
        0b10010101

    };

    public static HashSet<int> wallBottmEightDirections = new HashSet<int>
    {
        0b01000001
    };
}
