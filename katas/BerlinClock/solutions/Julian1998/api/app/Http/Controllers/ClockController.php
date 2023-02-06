<?php

namespace App\Http\Controllers;

use Carbon\Carbon;
use DateTime;
use DateTimeZone;
use Exception;
use Illuminate\Http\JsonResponse;

class ClockController extends Controller
{
    private int $base = 5;

    /**
     * Return berlin clock representation for given timezone ($continent/$city)
     *
     * @param string $continent
     * @param string $city
     * @return JsonResponse
     * @throws Exception
     */
    public function get(string $continent, string $city): JsonResponse
    {
        //get current datetime for given timezone
        $datetime = new Carbon("now", new DateTimeZone("$continent/$city") );

        return response()->json([
            'hour1' => $this->firstBar($datetime),
            'hour2' => $this->secondBar($datetime),
            'minute1' => $this->thirdBar($datetime),
            'minute2' => $this->fourthBar($datetime),
        ]);
    }

    /**
     * Returns first bar (0-20 hours) of the berlin clock as an array ("true" if bar should be set, "false" if it should not)
     *
     * Example for "16:31":
     * [true, true, true, false]
     *
     * @param DateTime $dt
     * @return array
     */
    private function firstBar(DateTime $dt): array
    {
        $hours = $dt->format('H');
        $hours = (int) ($hours / $this->base);

        $result = [false, false, false, false];
        for ($i = 0; $i < $hours; $i++) {
            $result[$i] = true;
        }

        return $result;
    }

    /**
     * Returns second bar (0-4 hours) of the berlin clock as an array ("true" if bar should be set, "false" if it should not)
     *
     * Example for "16:31":
     * [true, false, false, false]
     *
     * @param DateTime $dt
     * @return array
     */
    private function secondBar(DateTime $dt): array
    {
        $hours = $dt->format('H');
        $hours %= $this->base;

        $result = [false, false, false, false];
        for ($i = 0; $i < $hours; $i++) {
            $result[$i] = true;
        }

        return $result;
    }

    /**
     * Returns third bar (0-55 minutes) of the berlin clock as an array ("true" if bar should be set, "false" if it should not)
     *
     * Example for "16:31":
     * [true, true, true, true, true, true, false, false, false, false, false]
     *
     * @param DateTime $dt
     * @return array
     */
    private function thirdBar(DateTime $dt): array
    {
        $minutes = $dt->format('i');
        $minutes = (int) ($minutes / $this->base);

        $result = [false, false, false, false, false, false, false, false, false, false, false];
        for ($i = 0; $i < $minutes; $i++) {
            $result[$i] = true;
        }

        return $result;
    }

    /**
     * Returns fourth bar (0-4 minutes) of the berlin clock as an array ("true" if bar should be set, "false" if it should not)
     *
     * Example for "16:31":
     * [true, false, false, false]
     *
     * @param DateTime $dt
     * @return array
     */
    private function fourthBar(DateTime $dt): array
    {
        $minutes = $dt->format('i');
        $minutes %= $this->base;

        $result = [false, false, false, false];
        for ($i = 0; $i < $minutes; $i++) {
            $result[$i] = true;
        }

        return $result;
    }
}
