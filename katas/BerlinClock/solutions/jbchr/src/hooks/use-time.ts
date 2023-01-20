import { useEffect, useState } from "react";
import { BerlinClockTime } from "../models/berlin-clock";
import { getBerlineClockTime } from "../utils/time";

/**
 * Regularly fetches the berlin time from the server.
 * @param refreshRate
 * @returns berlin time
 */
export function useTime(refreshRate: number = 1000) {
  const [time, setTime] = useState<BerlinClockTime>();

  useEffect(() => {
    const interval = setInterval(() => {
      fetch("/api/berlin-clock-time")
        .then((res) => res.json())
        .then((data) => setTime(data));
    }, refreshRate);

    return () => clearInterval(interval);
  }, [refreshRate]);

  return time;
}
