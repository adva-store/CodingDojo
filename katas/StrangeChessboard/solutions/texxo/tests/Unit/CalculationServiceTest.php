<?php
declare(strict_types=1);
namespace App\Tests;

use App\Services\CalculationService;
use PHPUnit\Framework\TestCase;

class CalculationServiceTest extends TestCase
{
    /**
     * @param array $columnStretch
     * @param array $rowStretch
     * @param array $expectedResult
     * @return void
     * @dataProvider provideTestDataPerColor
     */
    public function testCalculateTotalAreaForDifferentColors(array $columnStretch, array $rowStretch, array $expectedResult): void
    {
        // arrange
        $service = $this->getService();

        // act
        $result = $service->calculateTotalAreaForDifferentColors(
            columnStretch: $columnStretch,
            rowStretch: $rowStretch,
        );

        // assert
        self::assertEquals($expectedResult, $result);
        self::assertEquals(
            (array_sum($columnStretch) * array_sum($rowStretch)),
            ($result['total_black_area'] + $result['total_white_area'])
        );
    }

    /**
     * @return array[]
     */
    public static function provideTestDataPerColor(): array
    {
        return [
            [
                'columnStretch' => [3, 1, 2, 7, 1],
                'rowStretch' => [1, 8, 4, 5, 2],
                'expectedResult' => [
                    'total_white_area' => 146,
                    'total_black_area' => 134
                ]
            ],
            // more test-data could / should follow
        ];
    }

    /**
     * @return CalculationService
     */
    private function getService(): CalculationService
    {
        return new CalculationService();
    }
}