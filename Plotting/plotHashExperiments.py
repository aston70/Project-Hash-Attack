import pandas as pd
import matplotlib.pyplot as plt
import numpy as np
import glob

# Helper: compute expected
def expectedCollision(n):
    return 2**(n/2)
def expectedPreimage(n):
    return 2**n

# Load multiple CSV files
def loadCsvFiles(pattern):
    files = glob.glob(pattern)
    if len(files) == 0:
        print(f"No CSV files found matching pattern: {pattern}")
        return pd.DataFrame() 
    dfs = [pd.read_csv(f) for f in files]
    return pd.concat(dfs, ignore_index = True)

# Plot graphs
def plotBoxWhisker(df, attackType, expected_fn, outfname):
    
    if df.empty:
        print(f"No data to plot for {attackType}")
        return    
    
    bit_sizes = sorted(df["BitSize"].unique())
    data = [df[df["BitSize"]==n]["NumberOfAttempts"].values for n in bit_sizes]

    fig, ax = plt.subplots(figsize=(10,6))
    ax.boxplot(data, showfliers=False)

    # Set x-axis labels
    ax.set_xticklabels(bit_sizes)
    ax.set_title(f"{attackType} Attack: Attempts by Bit Size")
    
    ax.set_ylabel("Number of Attempts")
    ax.set_xlabel("Hash Bit Size")
    
    # Scatter all samples for distribution
    for i, n in enumerate(bit_sizes):
        y = df[df["BitSize"]==n]["NumberOfAttempts"].values
        x = np.random.normal(i+1, 0.06, size=len(y))
        ax.scatter(x, y, alpha=0.6, s=8)

    # Expected values line
    expectedValues = [expected_fn(n) for n in bit_sizes]
    label = r"Theoretical expected ($2^{n/2}$)" if attackType=="Collision" else r"Theoretical expected ($2^n$)"
    ax.plot(range(1,len(bit_sizes)+1), expectedValues, marker='o', linestyle='--', label=label)

    ax.set_yscale('log')
    ax.set_xlabel("Bit size (n)")
    ax.set_ylabel("Number of Attempts (log scale)")
    ax.set_title(f"{attackType} Attack : attempts by bit size")
    ax.legend()
    plt.tight_layout()
    plt.savefig(outfname, dpi=200)
    print("Saved", outfname)

# Load all trial CSVs
collisionData = loadCsvFiles("collision_results_trial_*.csv")
print(collisionData.columns)
preimageData = loadCsvFiles("preimage_results_trial_*.csv")

# Plot results
plotBoxWhisker(collisionData, "Collision", expectedCollision, "collision_plot.png")
plotBoxWhisker(preimageData, "Preimage", expectedPreimage, "preimage_plot.png")
