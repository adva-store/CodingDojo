<?php
declare(strict_types=1);

namespace App\Services;

class CalculationService
{
    /**
     * @param array $columnStretch
     * @param array $rowStretch
     * @return int[]
     */
    public function calculateTotalAreaForDifferentColors(
        array $columnStretch,
        array $rowStretch
    ): array
    {
        $totalWhiteFieldArea = 0;
        $totalBlackFieldArea = 0;

        // iterating over every field of the chess field from top left to bottom right (column per column) and alternately add the current value to one of the two colorCounters (defined above)
        foreach ($columnStretch as $columnIndex => $column) {
            foreach ($rowStretch as $rowIndex => $row) {
                if (($columnIndex+$rowIndex) % 2 === 0) {
                    $totalWhiteFieldArea += round(num: $column * $row);
                } elseif (($columnIndex+$rowIndex) % 2 !== 0) {
                    $totalBlackFieldArea += round(num: $column * $row);
                }
            }
        }

        return ['total_white_area' => $totalWhiteFieldArea, 'total_black_area' => $totalBlackFieldArea];
    }
}