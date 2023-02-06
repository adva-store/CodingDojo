<?php

namespace Tests\Feature;

use Carbon\Carbon;
use Generator;
use Tests\TestCase;

class BerlinClockTest extends TestCase
{
    /**
     * @dataProvider provider
     */
    public function test($continent, $city, $expected)
    {
        $testDate = Carbon::create(2023, 2, 5, 16, 31, 0, "UTC");
        Carbon::setTestNow($testDate);

        $response = $this->get("/api/clock/$continent/$city");

        $response->assertStatus(200);
        $response->assertJson($expected);
    }

    public function provider(): Generator
    {
        yield "Europe/Berlin" => [
            'Europe',
            "Berlin",
            ["hour1"=>[true,true,true,false],"hour2"=>[true,true,false,false],"minute1"=>[true,true,true,true,true,true,false,false,false,false,false],"minute2"=>[true,false,false,false]]
        ];
        yield "America/New_York" => [
            'america',
            "new_york",
            ["hour1"=>[true,true,false,false],"hour2"=>[true,false,false,false],"minute1"=>[true,true,true,true,true,true,false,false,false,false,false],"minute2"=>[true,false,false,false]]
        ];
    }
}
