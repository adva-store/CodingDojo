<?php

namespace App\Service;

class ChessService
{
    /**
     * Summary of calculateResult
     * @param mixed $columns
     * @param mixed $rows
     * @return array
     */
    public function calculateResult($columns, $rows): array
    {
        $counter = 0;
        $white = 0;
        $black = 0;
        foreach ($rows as $row) {
            foreach ($columns as $column) {
                if($counter % 2 == 0){
                    $white += $row * $column;
                }
                else{
                    $black += $row * $column;
                }
                $counter++;
            }
        }

        return [
            'white' => $white, 
            'black' => $black
        ];
    }

    /**
     * Summary of printResult
     * @param mixed $columns
     * @param mixed $rows
     * @return array
     */
    public function printResult($columns, $rows): array
    {
        $sorted = [];
        $flag = 0;
        $counter = 0;
        $printWidth = 1000; // 1000px

        foreach ($rows as $row) {
            $sorted[$counter] = [];
            $colSum = 0;
            foreach ($columns as $column) {
                if ($flag === 0) {
                    $flag = 1;
                } else {
                    $flag = 0;
                }
                array_push($sorted[$counter], [
                    'flag' => $flag,
                    'column' => $column,
                    'row' => $row,
                    'surface' => $column * $row
                ]);
                $colSum += $column;
            }
            $counter++;
        }

        $coefficient = $printWidth / $colSum;
        
        return [$printWidth, $coefficient, $sorted];
    }
}